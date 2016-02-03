function addMessage_me(msg, author, time) {
    document.getElementById("main_chat_box").innerHTML += "<li class='me'><div class='avatar-icon'></div><div class='messages'> <p>" + msg + "</p><span class='author'>" + author + " • </span><time>" + time + "</time> </div> </li>";

    
}

function addMessage_another(msg, author, time) {
    document.getElementById("main_chat_box").innerHTML += "<li class='another'><div class='avatar-icon'></div><div class='messages'> <p>" + msg + "</p><span class='author'>" + author + " • </span><time>" + time + "</time> </div> </li>";

    
}

function clearMessages() {
    document.getElementById("main_chat_box").innerHTML = "";
}
