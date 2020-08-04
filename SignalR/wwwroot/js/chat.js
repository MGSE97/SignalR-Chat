"use strict";

// Create connection to hub
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").withAutomaticReconnect().build();

// Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

// Handle Receive message
connection.on("ReceiveMessage", function(date, user, message) {
    // Styling magic
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\n/g,'<br/>');
    var encodedMsg = (user != null ? user + ": " : '')  + msg + '<small class="float-right">' + date + '</small>';

    var li = document.createElement("li");
    li.innerHTML = encodedMsg;
    li.className = 'clearfix';

    document.getElementById("messagesList").appendChild(li);
});

// Start connection
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    // Notice others we are here
    connection.invoke("Joined").catch(function(err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

// Handle send click
var sendFc = function(event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function(err) {
        return console.error(err.toString());
    }).then(() => {
        // Clean text box
        document.getElementById("messageInput").value = "";
    });
    event.preventDefault();
};

document.getElementById("sendButton").addEventListener("click", sendFc);
document.getElementById("messageInput").addEventListener("keyup", function(event){
    if (event.keyCode === 13) {
        sendFc(event);
    }
});