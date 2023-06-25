var UserId = document.getElementById("UserId").value;
console.log(`SenderId: ${UserId}`);
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/*start SendMessage*/
connection.on("ReceiveMessage", function (user, message, Path,UserID,GroupId,PersonID) {
    var dateMsg = new Date();
    var dateMean = dateMsg.getHours() + ":" + dateMsg.getMinutes() + ":" + dateMsg.getUTCDate();
    //var ImgPath = document.getElementById("imgPath").value;
    console.log(`ResverId: ${UserId}`);
    if (UserId != UserID) {


   
        var msg = 
           ` <div class="d-flex flex-row justify-content-end mb-4" >
                                <div class="p-3 me-3 border" style="border-radius: 15px; background-color: #fbfbfb;">
                                    <p class="small mb-0">${message}</p>
                                </div>
                                <img src="${Path}"
                                     alt="avatar 1" style="width: 45px; height: 100%;">
                            </div>
    `;
    } else {
        var msg = ` <div class="d-flex flex-row justify-content-start mb-4">
                                <img src="${Path}"
                                     alt="avatar 1" style="width: 45px; height: 100%;">
                                <div class="p-3 me-3" style="border-radius: 15px; background-color: rgba(57, 192, 237,.2);">
                                    <p class="small mb-0">
                                       ${message}
                                    </p>
                                </div>
                            </div>`;
    }
  
  


    $("#list").append(msg);
});

connection.start();

$("#btnSend").on("click", function () {
    var user = $("#txtUser").val();
    var message = $("#txtMessage").val();
    var Path = $("#imgPath").val();
    var UserId = $("#UserId").val();
    var Userinfo = $("#UserInfoID").val();
    var GroupId = $("#groupID").val();
    connection.invoke("SendMessage", user, message, Path, UserId, GroupId, Userinfo);
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