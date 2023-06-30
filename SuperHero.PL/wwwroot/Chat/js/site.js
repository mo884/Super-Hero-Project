var UserId = document.getElementById("UserId").value;
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveUser", function (userId, message, SenderID, Path, NameUser) {

    // Handle received message
    if (UserId == SenderID) {
       
        if (userId != SenderID) {


          
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

    } 
    if (userId == UserId) {
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

connection.start().catch(function (err) {
    console.error(err.toString());
});

function sendToMessage(userId, message, SenderID, Path, NameUser) {
    connection.invoke("SendToMessage", userId, message, SenderID, Path, NameUser).catch(function (err) {
        console.error(err.toString());
    });
}

