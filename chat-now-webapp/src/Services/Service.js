import axios from "axios";

const apiPort = "5106";
const localApi = `http://localhost:${apiPort}/api/`;
const jsonServerApi = `http://localhost:3000/`;

const api = axios.create({
  baseURL: localApi,
});

export default api;
