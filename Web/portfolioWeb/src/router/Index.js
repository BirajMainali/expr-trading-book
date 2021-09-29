import {createRouter, createWebHistory} from 'vue-router'
import Dashboard from "../Views/Dashboard.vue";
import Publish from "../Views/Publish.vue";
import Trading from "../Views/Trading.vue"
import Portfolio from "../Views/Portfolio.vue"


const routes = [{
    path: "/",
    name: Dashboard,
    component: Dashboard
},
    {
        path: "/Publish",
        name: Publish,
        component: Publish

    }, {
    path: "/Trading",
    name : Trading,
    component: Trading
    },{
        path: "/Portfolio",
        name : Portfolio,
        component: Portfolio
    }]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;