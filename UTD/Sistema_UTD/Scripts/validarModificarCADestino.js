'use strict';

// 1. Declaración las variables globales
let txtCuatrigrama, txtDestino;

// Lógica para cambiar el texto del botón Aceptar (feedback)
document.addEventListener("DOMContentLoaded", function () {
    // Verificamos que el ScriptManager de ASP.NET esté cargado
    if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        // Se ejecuta apenas el usuario hace clic y la petición viaja al servidor
        prm.add_beginRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptarModificarRango'); // Asegúrate de tener ClientIDMode="Static"
            if (btnAceptar) {
                btnAceptar.disabled = true;
                btnAceptar.value = "Guardando..."; // Feedback visual de espera
            }
        });

        // Se ejecuta cuando el servidor devuelve la respuesta (éxito o error)
        prm.add_endRequest(function (sender, args) {
            var btnAceptar = document.getElementById('btnAceptarModificarRango');
            if (btnAceptar) {
                btnAceptar.disabled = false;
                btnAceptar.value = "Aceptar";
            }
        });
    }
});

// 2. Función para enganchar eventos (preparada para el UpdatePanel)
function enlazarEventos() {
    // Volvemos a buscar los controles en la pantalla (porque pudieron ser recreados por C#)
    txtCuatrigrama = document.getElementById("txtCuatrigrama");
    txtDestino = document.getElementById("txtDestino");

    // Metemos los campos en un arreglo para no repetir código
    let campos = [txtCuatrigrama, txtDestino];

    // Recorremos cada campo y le asignamos los eventos
    campos.forEach(campo => {
        if (campo) {
            // Evento 'input': Valida mientras el usuario teclea
            campo.addEventListener('input', () => validarCampo(campo));

            // Evento 'blur': Valida cuando el usuario presiona TAB o hace clic fuera del campo
            campo.addEventListener('blur', () => validarCampo(campo));
        }
    });
}

// 3. Manejo del ciclo de vida de ASP.NET
if (typeof Sys !== 'undefined') {
    Sys.Application.add_load(enlazarEventos);
} else {
    document.addEventListener("DOMContentLoaded", enlazarEventos);
}

// 4. Función que maneja el switch general (Modelo Activo/Inactivo)
function cambiarCamposDestino(checkbox) {
    let camposGenerales = [txtCuatrigrama, txtDestino];
    let estaActivo = checkbox.checked;

    // Habilitar o deshabilitar campos generales
    camposGenerales.forEach(campo => {
        if (campo) {
            campo.disabled = !estaActivo;
            // Si se acaba de deshabilitarlo, se le quita los colores de validación
            if (!estaActivo) {
                campo.classList.remove("is-valid", "is-invalid");
            }
        }
    });
}

// 6. Validación individual modificada
function validarCampo(campo) {
    if (!campo) return false;

    // Se valida normal si el campo está habilitado
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

// 7. Función principal para el botón Aceptar
function validarDestinoModificado(checkboxDestino) {
    let estaActivoDestino = checkboxDestino.checked;

    if (!estaActivoDestino) {
        return true; // Todo en orden, para desactivar al usuario
    } else {
        enlazarEventos(); // Forzamos actualización de variables

        const cuatrigramaValido = validarCampo(txtCuatrigrama);
        const destinoValido = validarCampo(txtDestino);

        // Si ALGUNO es falso (y no estaba deshabilitado), bloqueamos el guardado
        if (!cuatrigramaValido || !destinoValido) {
            return false;
        }

        return true; // Todo en orden
    }
}