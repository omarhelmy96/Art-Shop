function makeNav() {
    

    if (window.scrollY > 250) {

        document.getElementById("MainNavBar").style.background = "white";
        document.getElementById("MainNavBar").style.position = "fixed";
        document.getElementById("MainNavBar").style.width = "100%";
        document.getElementById("MainNavBar").style.left = "0";
        document.getElementById('MainNavBar').style.top = '0';
    }
    else {
        document.getElementById("MainNavBar").style.background = "none";
        document.getElementById("MainNavBar").style.position = "static";


    }
}


