import Vue from "vue"
import Home from "views/Home/Home.vue"


//Add all of the fontawesome icons
//TODO: Figure out if I always want to load the icons
//      or do it on a module by module basis, for now this works
import fontawesome from '@fortawesome/fontawesome'
import solid from "@fortawesome/fontawesome-free-solid"
fontawesome.library.add(solid);

new Vue({
    el: "#app",
    render: h => h(Home)
});