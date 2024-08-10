$(function () {
    var chatHub = $.connection.chatHub;

    chatHub.client.sendMessage = function (message) {
        console.log("Received message:", message);
    };

    $.connection.hub.start().done(function () {
        console.log("SignalR connected.");

        $(".sendMessageLink").on("click", function (e) {
            e.preventDefault();
            var employeeId = $(this).data("employee-id");
            var message = prompt("Enter your message:");
            if (message) {
                chatHub.server.sendMessage(message); // Send the message to the server
            }
        });
    });
});
