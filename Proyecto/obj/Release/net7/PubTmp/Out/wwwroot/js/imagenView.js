
var imagen = document.getElementById("imagen");
var previzualizar = document.getElementById("previzualizar");
var loading = document.querySelector(".loading");






imagen.addEventListener("change", ()=> {
    loading.style.display = "flex";
    var urlImagen = imagen.files[0];

    console.log(urlImagen);
    previzualizar.style.transition = "all 1000ms ease-out";
    var datosImgen = new FileReader;
    datosImgen.readAsDataURL(urlImagen);


    datosImgen.addEventListener("load",(e) => {


        setTimeout(
            

       loads, 5000);

       
        function loads() {
            var rutaImagen = e.target.result;
            previzualizar.setAttribute("src", rutaImagen);
            previzualizar.style.opacity = 1;
            previzualizar.style.transition = "all 3000ms ease";
            loading.style.opacity = 0;
            loading.style.transition = "all 300ms ease";
       }

    });
});
