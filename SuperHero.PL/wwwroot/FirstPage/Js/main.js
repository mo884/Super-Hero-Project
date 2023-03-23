const navbar = document.querySelector('#navbar');
const toggle = () =>{
    if (navbar.style.width == '220px') {
        navbar.style.width = '0';
    }
    else{
        navbar.style.width = "220px";
    }
}
