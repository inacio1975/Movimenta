function docReady(fn) {
    if (document.readyState === 'complete' || document.readyState === 'interactive') {
        setTimeout(fn, 1);
    } else {
        document.addEventListener('DOMContentLoaded', fn);
    }
}

const fotoPerfil = document.getElementById('navbarDropdown');
if(fotoPerfil){
fotoPerfil.addEventListener('click', () => {
    const userOptions = document.getElementById('user-options');
    if (userOptions.style.display === '' || userOptions.style.display === 'none')
        userOptions.style.display = 'block';
    else {
        userOptions.style.display = 'none';
    }
});
}

const projMenu = document.querySelectorAll('#v-pills-tab .nav-item');
const tabs = document.querySelectorAll('#v-pills-tabContent .tab-pane');
projMenu.forEach((item, index) => {
    //item.classList.remove('active');
       
    item.addEventListener('click', () => {
        projMenu.forEach((element) => {
            element.getElementsByTagName('a')[0].classList.remove('active');
        });
        item.getElementsByTagName('a')[0].classList.add('active');

        tabs.forEach((tab) => {
            tab.classList.remove('active');
        });
        tabs[index].classList.add('active');
    });
});

const btnEditarCategoria = document.querySelectorAll(".editarcategoria");
btnEditarCategoria.forEach(function (btn) {
    btn.addEventListener("click", () => {
        document.getElementById("txtcategoriaedit").value = btn.getAttribute("data-name");
        document.getElementById("idcategoria").value = btn.getAttribute("data-path");
    });
});

const btnEliminarEvento = document.querySelectorAll(".btnEliminarEvento");
btnEliminarEvento.forEach(function(btn) {
    btn.addEventListener("click", () => {
        document.getElementById("idevento").value = btn.getAttribute("data-id");
    });
});

function setEditForm(result) {
    var form = document.getElementById("editarEventoForm");
    form.querySelector("#idEvento").value = result.EventoId;
    form.querySelector("#TituloEvento").value = result.Titulo;
    form.querySelector("#shortdesc").value = result.DescricaoCurta;
    form.querySelector("#longdesc").value = result.DescricaoLarga;
    form.querySelector("#local").value = result.Local;
    //form.querySelector("#data").value = correctDate(result.Data);
   
    form.querySelector("#data").value = "12/04/2021";
    $("#loca").val(correctDate(result.Data));
    console.log("done... performed");

}

const btnEditarEvento = document.querySelectorAll(".btnEditarEvento");
btnEditarEvento.forEach(function(btn) {
    btn.addEventListener("click", () => {
        const id = btn.getAttribute("data-id");
        
        $.ajax({
            type: "POST",
            url: '/Eventos/Evento',
            data: {"id" : id},
            dataType: "application/json",
            sucess: function (result) {
                console.log("sucess");
                console.log(JSON.parse(result));
            },
            error: function (result) {
                let evento = JSON.parse(result.responseText);
                console.log("error");
                console.log(result);
                console.log(evento);
                setEditForm(evento);
            },
            timeout: 15000
        });

        
    });
});

function correctDate(d) {
    let m = d.match(/\/Date\((\d+)\)\//);
    return m ? (new Date(m[1])).toLocaleDateString("MM/dd/yyyy") : d;
}

