using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace _408ProjectStep1_server
{
    public partial class Form1 : Form
    {
        // Variables:
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> clientUsernames = new List<string>();
        FolderBrowserDialog storageFile;
        string dataBaseFilePath;

        bool terminating = false;
        bool listening = false;
  
        public Form1()
        {
            // Initialize the form
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();

            logs.AppendText("Please select a storage path to start listening.\n");
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ensures that when form is closed it doesn't crash
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                // Starts listening on the port given from gui
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(100);

                listening = true;
                button_listen.Enabled = false;

                // Create a new thread which is going to accept clients
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                // Display message
                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                // If port number from gui can't be parsed into an interger
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            // Continues to execute while listening on a port
            while (listening)
            {
                try
                {
                    // If there is a client, try to accept it.
                    // If it is accepted get the client's username
                    Socket newClient = serverSocket.Accept();
                    Byte[] buffer = new Byte[64];
                    newClient.Receive(buffer);

                    // Parse the username
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    string username = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // Check if a client with the same username is already connected
                    if(clientUsernames.Contains(username))
                    {
                        // If username exists in the server's connected clients list
                        // Display necessary messages
                        logs.AppendText(username + " tried to connect.\n");
                        logs.AppendText("Already existing username, connection not accepted!\n");

                        // Close the socket from servers side ie.disconnect from client
                        newClient.Shutdown(SocketShutdown.Both);
                        newClient.Close();
                    }

                    else
                    {
                        // If client with the username doesn't exist server is coonected to the accepted client
                        // Add the client and its username to list and display message
                        clientSockets.Add(newClient);
                        clientUsernames.Add(username);
                        logs.AppendText("A client with username " + username + " is connected.\n");
                                  
                        // Create a recieve thread for the client and start it       
                        Thread receiveThread = new Thread(() => Receive(newClient));
                        receiveThread.Start();
                    }


                    
                }
                catch
                {
                    // If server can't accept  client
                    if (terminating)
                    {
                        // If server is terminating, make listening false
                        listening = false;
                    }
                    else
                    {
                        // If server is not terminating, display message
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient)
        {
            // Thread that recieves files from clients
            bool connected = true;
            string name = clientUsernames[clientSockets.IndexOf(thisClient)];
            string receivePath = storageFile.SelectedPath;
            string fileName = "";
            string fileData = "";

            while (connected && !terminating )
            {
                // Execute while client is connected and server is not terminating
                try
                {
                    // Get the data sent from client
                    Byte[] clientData = new Byte[1024 * 50000];
                    int receivedBytesLen = thisClient.Receive(clientData);

                    // Parse the data
                    int fileNameLen = BitConverter.ToInt32(clientData, 0);
                    fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                    fileData = Encoding.ASCII.GetString(clientData, 4 + fileNameLen, receivedBytesLen);

                    // If received file data is not empty and client is connected
                    // Write the data into file and save it to the specified location
                    if (fileData != "" && connected)
                        saveFile(fileData, receivePath, name, fileName);
                }                

                catch
                {
                    // If an error is thrown while receiving data
                    if (!terminating )
                    {
                        // If server is not terminating, display message saying client has disconnected
                        logs.AppendText(name + " has disconnected\n");
                    }

                    try
                    {
                        // Disconnect from client, remove the client and its username from list
                        connected = false;
                        thisClient.Close();
                        clientUsernames.RemoveAt(clientSockets.IndexOf(thisClient));
                        clientSockets.Remove(thisClient);                        
                    }

                    catch
                    {
                        logs.AppendText("Couldn't remove client socket!\n");
                    }
                    
                }
            }
        }

        private void saveFile(string data, string receivePath, string client, string fname)
        {
            // Save the file to specified database location
            logs.AppendText("File received succesfully from " + client + "\n");

            string filePath = receivePath + "\\" + client + fname;

            // If same file exists in that location add a counter to the
            // end of the file name then save it
            if (File.Exists(filePath))
            {
                // Display message saying this file exists, saved as something else
                logs.AppendText("Existing file, ");

                int counter = 1;
                string name = fname.Substring(0, fname.LastIndexOf('.'));
                string ext = fname.Substring(fname.LastIndexOf('.'));

                while (File.Exists(filePath))
                {
                    fname = name + "-" + counter.ToString() + ext;
                    filePath = receivePath + "\\" + client + fname;

                    counter++;
                }

                // Write the received content to file
                File.WriteAllText(filePath, data);
                logs.AppendText("contents are saved in: " + client + fname + "\n");
            }   

            // If file doesn't exists just write it
            else
                File.WriteAllText(filePath, data);

            // Update the database file
            DateTime utcDate = DateTime.UtcNow;
            string updateDB = client + " " + fname + utcDate + ", Utc\n";

            // If file exists, adds a new line with the received file's information
            // If it doesn't creates a new database file and then adds a new line
            // with the received file's information
            using (StreamWriter sw = File.AppendText(dataBaseFilePath))
            {
                sw.WriteLine(updateDB);
            }
            
            
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            // Lets server to choose where the received files will be saved
            // through gui
            storageFile = new FolderBrowserDialog();

            if (storageFile.ShowDialog() == DialogResult.OK)
            {
                // If a location is selected
                textBox_path.Text = storageFile.SelectedPath;
                logs.AppendText("Storage path is selected.\n");

                // Creates a database file in the selected location
                if (!File.Exists(storageFile.SelectedPath + "\\DBFile.txt"))
                {
                    FileStream dbFile;
                    dbFile = File.Create(storageFile.SelectedPath + "\\DBFile.txt");
                }
                dataBaseFilePath = storageFile.SelectedPath + "\\DBFile.txt";
                button_listen.Enabled = true;              
            }
        }
    }
}
