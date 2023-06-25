var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/*start SendMessage*/
connection.on("ReceiveMessage", function (user, message, Path) {
    var dateMsg = new Date();
    var dateMean = dateMsg.getHours() + ":" + dateMsg.getMinutes() + ":" + dateMsg.getUTCDate();
    var ImgPath = document.getElementById("imgPath").value;
    var msg = `<div class="alert alert-primary" role="alert"><img src='${Path}' style='    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    margin-right: 10px;'/>
 <b> ${user}</b> : ${message} &ensp; ${dateMean}
</div>`;
  


    $("#list").append(msg);
});

connection.start();

$("#btnSend").on("click", function () {
    var user = $("#txtUser").val();
    var message = $("#txtMessage").val();
    var Path = $("#imgPath").val();
    connection.invoke("SendMessage", user, message,Path);
    $("#txtMessage").val('');
});

/*end SendMessage*/

/*start JoinGroup*/
connection.on("GroupMessage", function (name, group) {  
    var msg = name + " joinned " + group; 
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);
});

$("#btngroup").on("click", function () {
    var name = $("#txtUser").val(); 
    var group = $("#txtgroup").val();

    connection.invoke("JoinGroup", group, name);

});
/*end JoinGroup*/

/*start GroupSendToMessage*/
connection.on("GroupSendToMessage", function (group,name, message) {
    var msg = name + "(" + group + "):" + message; 
    var li = document.createElement("li");
    li.textContent = msg;

    $("#list").prepend(li);  
});
 
$("#txtSendGroup").on("click", function () {    
   
    var name = $("#txtUser").val();
    var group = $("#txtgroup").val(); 
    var message = $("#txtMessage").val();
    connection.invoke("SendToGroup", name, group, message);  
});
/*end GroupSendToMessage*/