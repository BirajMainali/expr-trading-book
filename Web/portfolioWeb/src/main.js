import {createApp} from 'vue'
import App from './App.vue'
import router from "./router";
import axios from "axios";
import Toaster from "@meforma/vue-toaster";

axios.defaults.baseURL = "https://localhost:5001";
createApp(App).use(router).use(Toaster).mount('#app')
