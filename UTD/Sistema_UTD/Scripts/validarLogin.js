'use strict';

// 1. Declaramos las variables globales
let txtUsuario;
let txtContrasenia;

// 2. Esperar a que el HTML (DOM) cargue completamente
document.addEventListener("DOMContentLoaded", function () {
    // Capturamos los controles (gracias a ClientIDMode="Static")
    txtUsuario = document.getElementById("txtUsuario");
    txtContrasenia = document.getElementById("txtContrasenia");

    // Asignar los eventos para validación en tiempo real
    if (txtUsuario) {
        txtUsuario.addEventListener('input', () => validarCampo(txtUsuario));
        txtUsuario.addEventListener('blur', () => validarCampo(txtUsuario));
    }
    if (txtContrasenia) {
        txtContrasenia.addEventListener('input', () => validarCampo(txtContrasenia));
        txtContrasenia.addEventListener('blur', () => validarCampo(txtContrasenia));
    }
});

// 3. Función genérica que valida un solo campo
function validarCampo(campo) {
    if (!campo) return false;

    if (campo.value.trim() === "") {
        campo.classList.remove("is-valid");
        campo.classList.add("is-invalid");
        return false;
    } else {
        campo.classList.remove("is-invalid");
        campo.classList.add("is-valid");
        return true;
    }
}

// 4. Función principal que se dispara al hacer clic en "Ingresar"
function validarLogin() {
    // Ejecutamos validaciones
    const usuarioValido = validarCampo(txtUsuario);
    const contraseniaValido = validarCampo(txtContrasenia);

    // Si algún campo está vacío, detenemos todo y no viajamos al servidor
    if (!usuarioValido || !contraseniaValido) {
        return false;
    }

    // Si todo está correcto, aplicamos el feedback visual al botón
    var btnAceptar = document.getElementById('btnIngresar');
    if (btnAceptar) {
        btnAceptar.value = "Iniciando sesión..."; // Cambiamos el texto

        // Retrasamos el bloqueo del botón unos milisegundos
        setTimeout(function () {
            btnAceptar.disabled = true;
        }, 50);
    }

    return true; // Permite que el PostBack viaje al CodeBehind de C#
}