import React, { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';

function App() {
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5087/NotificationHub") // Replace with your SignalR hub URL
      .configureLogging(signalR.LogLevel.Information)
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(() => {
          console.log('SignalR connected');
        })
        .catch(error => console.error('SignalR connection error: ', error));
    }
  }, [connection]);

  const sendNotification = () => {
    if (connection && connection.state === signalR.HubConnectionState.Connected) {
      connection.invoke('SendMessage', 'User123', 'Hello from client')
        .then(() => console.log('Notification sent'))
        .catch(error => console.error('Error sending notification: ', error));
    } else {
      console.error('SignalR connection is not yet established or has been closed.');
    }
  };

  return (
    <div className="App">
      <h1>SignalR App</h1>
      <button onClick={sendNotification}>Send Notification</button>
      {/* Your other React components */}
    </div>
  );
}

export default App;
