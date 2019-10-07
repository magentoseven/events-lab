var hideChatWindow = function () {
    $('#chatbox').hide();
    setChatWindowState("OFF");
}

var showChatWindow = function (focus = true) {
    $('#chatbox').show();
    if (focus) {
        $('#chatinput').focus();
    }
    setChatWindowState("ON");
}

// Update chat log.
var updateChatLog = function (chatlog) {
    $("#chatlog").html('');
    chatlog.forEach(chat => {
        $('#chatlog').append(chatList(chat));
    });
    $("#chatscrollpanel").animate({ scrollTop: $('#chatscrollpanel').prop("scrollHeight") }, 50);
}

// Get chat log from Web API.
var getChatLog = function () {
    $.ajax({
        url: "/api/Chat",
        type: "GET",
        contentType: "application/json",
        complete: res => {
            var chatLog = res.responseJSON;
            updateChatLog(chatLog);
        }
    });
};

var getChatWindowState = function () {
    var chatWindowState = $.cookie('Event.Web.ChatWindowState');
    // console.log("GET WINDOW STATE: " + chatWindowState);
    if (!chatWindowState) {
        chatWindowState = "OFF";
        $.cookie('Event.Web.ChatWindowState', chatWindowState, { expires: 1, path: '/' });
    } else {
        chatWindowState = $.cookie('Event.Web.ChatWindowState');
    }

    return chatWindowState;
}

var setChatWindowState = function (chatWindowState) {
    $.cookie('Event.Web.ChatWindowState', chatWindowState, { expires: 1, path: '/' });
}

var chatList = function (chat) {
    var dateTime = new Date(chat.SentTime);
    var hou = ("0" + dateTime.getHours()).slice(-2);
    var min = ("0" + dateTime.getMinutes()).slice(-2);
    var timeDisplay = hou + ":" + min;
    return '<div class="row" style="padding:0; margin:0"><div style="width: 35px; padding-top: 7px"><span class="chat-time">' + timeDisplay + '</span></div><div style="width: 230px"><div class="chat-message"> ' + chat.Text + '</div></div></div>'
}

$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call back.
    chat.client.getChatLog = getChatLog;
    // Start the connection.
    $.connection.hub.start().done(function () {
        // Get chat log when ready.
        getChatLog();

        // Set send button click action.
        $('#chatsend').click(function () {
            // Call the Send method on the hub.

            // Send chat to Web API by Ajax.
            $.ajax({
                url: "/api/Chat",
                type: "POST",
                data: JSON.stringify({
                    Text: $('#chatinput').val()
                }),
                contentType: "application/json"
            });

            // Clear text box and reset focus for next comment.
            $('#chatinput').val('').focus();
        });
        // Set input enter key action.
        $('#chatinput').keyup(function (e) {
            if (e.keyCode == 13) {
                // Call the Send method on the hub.

                // Send chat to Web API by Ajax.
                $.ajax({
                    url: "/api/Chat",
                    type: "POST",
                    data: JSON.stringify({
                        Text: $('#chatinput').val()
                    }),
                    contentType: "application/json"
                });

                // Clear text box and reset focus for next comment.
                $('#chatinput').val('').focus();
            }
        });
    });

    // Chat window display toggle.
    $(".chat-title").click(function () {
        if (getChatWindowState() == "OFF") {
            showChatWindow(true);
        } else {
            hideChatWindow();
        }
    });
    if (getChatWindowState() == "OFF") {
        hideChatWindow();
    } else {
        showChatWindow(false);
    }
});