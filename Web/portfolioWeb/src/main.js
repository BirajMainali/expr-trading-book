import {createApp} from 'vue'
import App from './App.vue'
import router from "./router";
import axios from "axios";
import Notifications from '@kyvg/vue3-notification'

axios.defaults.baseURL = "https://localhost:5001";
createApp(App).use(router).use(Notifications).mount('#app')
