import {
  Button,
  Card,
  CardBody,
  CardHeader,
  Input,
  Typography,
} from "@material-tailwind/react";
import { GoogleLogin } from "@react-oauth/google";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

export const LoginForm = ({ onSubmit, signInWithGoogle, user, setUser }) => {
  const navigate = useNavigate();

  return (
    <Card shadow={false} className="md:px-24 md:py-14 py-8 bg-transparent">
      <CardHeader
        shadow={false}
        floated={false}
        className="text-center bg-transparent"
      >
        <Typography
          variant="h1"
          color="blue-gray"
          className="mb-4 !text-3xl lg:text-4xl text-white"
        >
          Chat Now
        </Typography>
        <Typography className="text-light-beige text-[18px] font-normal md:max-w-sm">
          Logue-se por email ou autenticação com Google!
        </Typography>
      </CardHeader>

      <CardBody>
        <form onSubmit={onSubmit} className="flex flex-col gap-4 md:mt-12">
          {/* Email Input */}
          <div>
            <label htmlFor="email">
              <Typography
                color="white"
                className="block font-medium mb-2 text-lg"
              >
                Email
              </Typography>
            </label>
            <Input
              id="email"
              color="black"
              size="lg"
              type="email"
              name="email"
              placeholder="nome@gmail.com"
              className="w-full bg-white mt-1 border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100"
              labelProps={{
                className: "hidden",
              }}
              value={user.email}
              onChange={(txt) => setUser({ ...user, email: txt.target.value })}
            />
          </div>

          {/* Password Inputs */}
          <div>
            <label htmlFor="password">
              <Typography
                variant="small"
                color="white"
                className="block font-medium mb-2 text-lg"
              >
                Senha
              </Typography>
            </label>
            <Input
              id="password"
              color="black"
              size="lg"
              type="password"
              name="password"
              placeholder="Insira sua melhor senha..."
              className="w-full bg-white mt-1 border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100"
              labelProps={{
                className: "hidden",
              }}
              value={user.password}
              onChange={(txt) =>
                setUser({ ...user, password: txt.target.value })
              }
            />
          </div>

          <Typography
            className="text-beige"
            onClick={() => {
              navigate("/cadastro");
            }}
          >
            Não tenho uma conta!
          </Typography>

          {/* Login EMAIL */}
          <Button
            size="lg"
            className="bg-light-purple mt-10"
            fullWidth
            type="submit"
          >
            continue
          </Button>

          {/* Login com Google */}
          <GoogleLogin
            onSuccess={(credentialResponse) => {
              setUser({
                ...user,
                googleId: credentialResponse.credential,
              });

              signInWithGoogle();
            }}
            onError={() => {
              alert("Login Failed");
            }}
          />

          {/* Message */}
          <Typography
            variant="small"
            className="text-center mx-auto max-w-[19rem] !font-medium !text-light-beige"
          >
            Upon signing in, you consent to abide by our{" "}
            <a href="#" className="text-beige">
              Terms of Service
            </a>{" "}
            &{" "}
            <a href="#" className="text-beige">
              Privacy Policy.
            </a>
          </Typography>
        </form>
      </CardBody>
    </Card>
  );
};

export const RegisterForm = ({
  user,
  signUpWithGoogle,
  setUser,
  signUpUser,
}) => {
  const navigate = useNavigate();

  // const handleGoogleSubmit = ({ credentialResponse }) => {
  //   const objReturn = jwtDecode(credentialResponse.credential);
  //   setUser({
  //     ...user,
  //     googleId: credentialResponse.credential,
  //     name: objReturn.given_name,
  //   });

  //   signUpWithGoogle();
  // };

  return (
    <Card shadow={false} className="md:px-24 md:py-14 py-8 bg-transparent">
      <CardHeader
        shadow={false}
        floated={false}
        className="text-center bg-transparent"
      >
        <Typography
          variant="h1"
          color="blue-gray"
          className="mb-4 !text-3xl lg:text-4xl text-white"
        >
          Chat Now
        </Typography>
        <Typography className="text-light-beige text-[18px] font-normal md:max-w-sm">
          Logue-se por email ou autenticação com Google!
        </Typography>
      </CardHeader>

      <CardBody>
        <form onSubmit={signUpUser} className="flex flex-col gap-4 md:mt-12">
          {/* Nome Input */}
          <div>
            <label htmlFor="name">
              <Typography
                color="white"
                className="block font-medium mb-2 text-lg"
              >
                Nome de usuário
              </Typography>
            </label>
            <Input
              id="name"
              color="black"
              size="lg"
              type="text"
              name="name"
              placeholder="Insira seu nome..."
              className="w-full bg-white mt-1 border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100"
              labelProps={{
                className: "hidden",
              }}
              value={user.name}
              onChange={(txt) => setUser({ ...user, name: txt.target.value })}
            />
          </div>

          {/* Email Input */}
          <div>
            <label htmlFor="email">
              <Typography
                color="white"
                className="block font-medium mb-2 text-lg"
              >
                Email
              </Typography>
            </label>
            <Input
              id="email"
              color="black"
              size="lg"
              type="email"
              name="email"
              placeholder="nome@gmail.com"
              className="w-full bg-white mt-1 border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100"
              labelProps={{
                className: "hidden",
              }}
              value={user.email}
              onChange={(txt) => setUser({ ...user, email: txt.target.value })}
            />
          </div>

          {/* Password Inputs */}
          <div>
            <label htmlFor="password">
              <Typography
                variant="small"
                color="white"
                className="block font-medium mb-2 text-lg"
              >
                Senha
              </Typography>
            </label>
            <Input
              id="password"
              color="black"
              size="lg"
              type="password"
              name="password"
              placeholder="Insira sua melhor senha..."
              className="w-full bg-white mt-1 border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100"
              labelProps={{
                className: "hidden",
              }}
              value={user.password}
              onChange={(txt) =>
                setUser({ ...user, password: txt.target.value })
              }
            />
            <Input
              id="passwordConfirm"
              color="black"
              size="lg"
              type="password"
              name="passwordConfirm"
              placeholder="Confirme sua senha..."
              className="w-full bg-white border-none placeholder:opacity-100 placeholder-blue-gray-900 !text-black !text-opacity-100 mt-[14px]"
              labelProps={{
                className: "hidden",
              }}
              value={user.confirmPassword}
              onChange={(txt) =>
                setUser({ ...user, confirmPassword: txt.target.value })
              }
            />
          </div>
          <Typography
            className="text-beige"
            onClick={() => {
              navigate("/");
            }}
          >
            Já tenho uma conta!
          </Typography>

          {/* Login EMAIL */}
          <Button
            size="lg"
            className="bg-light-purple mt-10"
            fullWidth
            type="submit"
          >
            continue
          </Button>

          {/* Login com o Google */}
          <GoogleLogin
            onSuccess={(credentialResponse) => {
              const objReturn = jwtDecode(credentialResponse.credential);
              setUser({
                ...user,
                name: objReturn.given_name,
                photoUrl: objReturn.picture,
                googleId: credentialResponse.credential,
              });

              signUpWithGoogle();
            }}
            onError={() => {
              alert("Login Failed");
            }}
          />

          {/* <GoogleLogin
            onSuccess={(credentialResponse) => {
              handleGoogleSubmit(credentialResponse);
            }}
            onError={() => {
              alert("Login Failed");
            }}
          /> */}

          {/* Message */}
          <Typography
            variant="small"
            className="text-center mx-auto max-w-[19rem] !font-medium !text-light-beige"
          >
            Upon signing in, you consent to abide by our{" "}
            <a href="#" className="text-beige">
              Terms of Service
            </a>{" "}
            &{" "}
            <a href="#" className="text-beige">
              Privacy Policy.
            </a>
          </Typography>
        </form>
      </CardBody>
    </Card>
  );
};
