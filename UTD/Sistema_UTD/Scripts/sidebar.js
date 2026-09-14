document.addEventListener("DOMContentLoaded", function () {
    const btnMenu = document.getElementById("btnToggleMenu");
    const contenedorSidebar = document.getElementById("contenedorSidebar");
    const spansSidebar = document.querySelectorAll("#contenedorSidebar span");
    const divInterno = contenedorSidebar.querySelector("div.d-flex.flex-column");
    const ulMenu = document.getElementById("menu");

    function aplicarEstadoSidebar(minimizar) {
        if (minimizar) {
            spansSidebar.forEach(function (span) { span.classList.remove("d-sm-inline"); });
            contenedorSidebar.classList.remove("col-md-3", "col-xl-2", "px-sm-2");
            if (divInterno) divInterno.classList.remove("align-items-sm-start");
            if (ulMenu) ulMenu.classList.remove("align-items-sm-start");
        } else {
            spansSidebar.forEach(function (span) { span.classList.add("d-sm-inline"); });
            contenedorSidebar.classList.add("col-md-3", "col-xl-2", "px-sm-2");
            if (divInterno) divInterno.classList.add("align-items-sm-start");
            if (ulMenu) ulMenu.classList.add("align-items-sm-start");
        }
    }

    // AL HACER CLIC: Cambiar el estado y guardarlo
    if (btnMenu) {
        btnMenu.addEventListener("click", function (e) {
            e.preventDefault();
            btnMenu.blur();

            // Leemos cómo está actualmente
            let estadoMinimizado = localStorage.getItem("sidebarMinimizada") === "true";

            // Lo invertimos
            estadoMinimizado = !estadoMinimizado;

            // Guardamos el nuevo estado en la memoria
            localStorage.setItem("sidebarMinimizada", estadoMinimizado);

            // Aplicamos los cambios visuales con animación
            aplicarEstadoSidebar(estadoMinimizado);
        });
    }
});