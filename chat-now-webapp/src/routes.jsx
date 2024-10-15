import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LoginPage } from "./Pages/LoginPage/LoginPage";
import { RegisterPage } from "./Pages/RegisterPage/RegisterPage";

export const Rotas = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route element={<LoginPage />} path="/" exact />
          <Route element={<RegisterPage />} path="/cadastro" />
        </Routes>
      </BrowserRouter>
    </div>
  );
};
