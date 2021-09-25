import {createRouter, createWebHistory} from 'vue-router'
import Dashboard from "../Views/Dashboard.vue";
import AddStock from "../Views/AddStock.vue";
import Trading from "../Views/Trading.vue"


const routes = [{
    path: "/",
    name: Dashboard,
    component: Dashboard
},
    {
        path: "/AddStock",
        name: AddStock,
        component: AddStock

    }, {
    path: "/Trading",
    name : Trading,
    component: Trading
    }]

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;