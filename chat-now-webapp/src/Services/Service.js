import axios from "axios";

const apiPort = '5173';
const localApi = `http://localhost:${apiPort}/api/`;

const api = axios.create({
    baseURL: localApi
});

export default api;