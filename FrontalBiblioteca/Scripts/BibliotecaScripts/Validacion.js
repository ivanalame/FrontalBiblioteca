﻿
function validarFormulario(e) {

    var usuario = document.getElementById("user").value;
    var password = document.getElementById("password").value;

    var regex = /^(?=.*[a-zA-Z])(?=.*\d).{6,}$/;

    // var passIncorrect = document.getElementById('pass-incorrect');
    var message = document.getElementById('message');

    var msgerror = "";
    ava
    //Validar que usuario y contraseña no sean iguales
    if (usuario === password) {
        // alert("El usuario y la contraseña no pueden ser iguales")
        msgerror = 'El usuario y la contraseña no pueden ser iguales'

    }

    // Validar que el contenido del nombre está todo en minúsculas y que como mínimo son 8 caracteres
    else if (usuario !== usuario.toLowerCase() || usuario.length < 8) {
        //alert("El nombre de usuario debe estar en minúsculas y tener al menos 8 caracteres.");
        msgerror = 'El nombre de usuario debe estar en minúsculas y tener al menos 8 caracteres.'

        //Esta NO evita el submit
        //return false;
        //Estas SÍ lo evitan

    }

    // Validar que la password tiene al menos 6 caracteres, con letras y dígitos
    else if (!regex.test(password)) {

        msgerror = "La Contraseña debe tener 6 carcateres con letras y digitos";
    }

    else {

        msgerror = "La Contraseña o el usuario no estan en la bbdd ";
    }
   
       

         

    if (msgerror !== "") {
        e.preventDefault();
        e.returnValue = false;
        message.textContent = msgerror;
    }
}



        //function checkCookie() {
        //    let idcliente = getCookie("user");
        //    if (idcliente != "") {
        //        alert("Welcome again " + user);
        //    } else {
        //        idcliente = prompt("Please enter your name:", "");
        //        if (idcliente != "" && idcliente != null) {
        //            setCookie("user", user, 365);
        //        }
        //    }
        //}
 