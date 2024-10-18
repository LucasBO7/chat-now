import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LoginPage } from "./Pages/LoginPage/LoginPage";
import { RegisterPage } from "./Pages/RegisterPage/RegisterPage";
import { HomePage } from "./Pages/Home/Home";

export const Rotas = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route element={<LoginPage />} path="/" exact />
          <Route element={<RegisterPage />} path="/cadastro" />
          <Route element={<HomePage />} path="/home" />
        </Routes>
      </BrowserRouter>
    </div>
  );
};
