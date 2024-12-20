import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LoginPage } from "./Pages/LoginPage/LoginPage";
import { RegisterPage } from "./Pages/RegisterPage/RegisterPage";
import { HomePage } from "./Pages/Home/HomePage";
import { FriendsPage } from "./Pages/FriendsPage/FriendsPage";
import { ChatPage } from "./Pages/ChatPage/ChatPage";

export const Rotas = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route element={<LoginPage />} path="/" exact />
          <Route element={<RegisterPage />} path="/cadastro" />
          <Route element={<HomePage />} path="/home" />
          <Route element={<FriendsPage />} path="/amigos" />
          <Route element={<ChatPage />} path="/chat"  />
        </Routes>
      </BrowserRouter>
    </div>
  );
};
