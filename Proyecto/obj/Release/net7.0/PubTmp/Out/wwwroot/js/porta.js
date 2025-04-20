var menu_list = document.querySelector(".menu_list");
var container_header = document.querySelector(".container_header");
var spanAlls = document.querySelectorAll("span");
var container_header_layout = document.querySelector(".container_header_layout");
var divLatestNew = document.querySelector(".divLatestNew"); 
var divStatico = document.querySelector(".divStatico");
var nav = document.querySelector("#navegador");
var players = document.querySelector(".header_gap");
var divLoad = document.querySelector(".divLoad");
var cardreveal = document.querySelectorAll(".cardreveal");
var observe = document.querySelectorAll(".observe");




//window.onscroll = function () {

//    var sc = window.scrollY;
//    let header = document.getElementById("idmaincontainer");
//    let headerLa = document.getElementById("headerLa");
//    if (sc > 30) {
//        header.classList.add("shadows");
//        headerLa.classList.add("shadows");
//    } else {
//        header.classList.remove("shadows");
//    }
//};
//menu_list.addEventListener("click", () => {
//    spanAlls.forEach((f, a) => {
//        f.classList.toggle(`linea${a + 1}__list`);
//    });
//    container_header.classList.toggle("left");
//});

window.addEventListener("load", (e) => {
    divLoad.style.transition = "all 1000ms ease";
    divLoad.style.opacity = 0;

    setTimeout(removeMainLoad, 2000);
})
const removeMainLoad = () => {

    divLoad.style.transition = "all 1000ms ease";
    divLoad.style.display = "none";
   
}
const arr=["luis","aaaa"]
function scrollLatest(entries, options) {

    var [entry] = entries;
   
    if (!entry.isIntersecting) {
      
        observe.forEach((i,v) => {

            i.classList.add(`observer_div${v}`);

        });
        return;
       
    }
    observe.forEach((i, v) => {

        i.classList.remove(`observer_div${v}`);

    });

    
    
}
const objetObserver={
    root:null,
    treshold:0
}

const headerObserve = (entries, options) => {
    var [entry] = entries;
   
   nav.classList.add('sticky');

    console.log(entry);

}
const revealCard = (entries, options) => {


    var[entry] = entries;

    entry.target.classList.remove('hiddencard');
    


}

const observerReveal = new IntersectionObserver(revealCard, { root: null, threshold:0.1 });
const observer = new IntersectionObserver(scrollLatest, objetObserver);
const observerHeader = new IntersectionObserver(headerObserve, {root:null,treshold:0} );
observer.observe(divLatestNew);
observerHeader.observe(players);

cardreveal.forEach(index => {
    observerReveal.observe(index);


})


