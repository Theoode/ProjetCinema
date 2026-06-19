import { useMutation } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import logo from "../../../assets/logo2.png";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import Input from "../../../components/Input";

const REGISTER_URL = "http://35.181.160.232:5000/register";
const LOGIN_URL = "http://35.181.160.232:5000/login";

const RegisterPage = () => {
  const [firstname, setFirstname] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [formSubmitted, setFormSubmitted] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (formSubmitted) {
      const newErrors: { [key: string]: string } = {};

      if (!firstname.trim()) newErrors.firstname = "Le prénom est requis.";
      if (!email.trim()) newErrors.email = "L'email est requis.";
      if (!password.trim()) newErrors.password = "Le mot de passe est requis.";
      if (!confirmPassword.trim())
        newErrors.confirmPassword = "Veuillez confirmer votre mot de passe.";

      if (password.length < 6) {
        newErrors.password =
          "Le mot de passe doit contenir au moins 6 caractères.";
      }

      if (password !== confirmPassword) {
        newErrors.confirmPassword = "Les mots de passe ne correspondent pas.";
      }

      setErrors(newErrors);
    }
  }, [firstname, email, password, confirmPassword, formSubmitted]);

  const registerMutation = useMutation({
    mutationFn: async () => {
      // Enregistrement de l'utilisateur
      const registerResponse = await fetch(REGISTER_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      const registerText = await registerResponse.text();
      const registerResult = registerText ? JSON.parse(registerText) : {};

      if (!registerResponse.ok) {
        const message = registerResult?.errors?.DuplicateUserName
          ? "Un compte avec cet email existe déjà."
          : registerResult?.errors?.Email?.[0] ||
            registerResult?.errors?.Password?.[0] ||
            registerResult?.title ||
            registerResult?.message ||
            "Échec de l'inscription, veuillez réessayer.";

        throw new Error(message);
      }

      // Connexion automatique après enregistrement
      const loginResponse = await fetch(LOGIN_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      if (!loginResponse.ok) {
        throw new Error("Échec de la connexion automatique.");
      }

      const loginData = await loginResponse.json();

      // Stockage du token dans localStorage
      localStorage.setItem("token", loginData.accessToken);
      localStorage.setItem(
        "user",
        JSON.stringify({
          email: email,
          token: loginData.accessToken,
        })
      );
    },
    onSuccess: () => {
      navigate("/");
    },
    onError: (error: any) => {
      setErrors((prev) => ({ ...prev, general: error.message }));
    },
  });

  const handleRegister = () => {
    setFormSubmitted(true);
    if (Object.keys(errors).length > 0) return;

    registerMutation.mutate();
  };

  return (
    <div className="flex flex-col lg:flex-row items-center justify-center min-h-screen text-white px-6 gap-12 lg:gap-32">
      <div className="relative p-10 lg:p-24">
        <div className="absolute inset-0 flex items-center justify-center">
          <div className="w-[200px] h-[200px] sm:w-[250px] sm:h-[250px] md:w-[300px] md:h-[300px] lg:w-[350px] lg:h-[350px] bg-primary opacity-40 rounded-full blur-3xl"></div>
        </div>
        <a href="/" className="relative z-10">
          <img
            src={logo}
            alt="Scryn Logo"
            className="h-40 sm:h-48 md:h-52 lg:h-72 transition-transform duration-300 transform hover:scale-105 cursor-pointer"
          />
        </a>
      </div>

      <Card className="w-full max-w-md sm:max-w-sm md:max-w-md p-6 sm:p-8 lg:p-10">
        <h2 className="text-2xl font-bold text-center">Inscription</h2>

        {errors.general && (
          <p className="text-red-500 text-sm text-center mt-2">
            {errors.general}
          </p>
        )}

        <label className="block text-sm font-medium mt-7">Prénom *</label>
        <Input
          type="text"
          placeholder="Prénom"
          value={firstname}
          onChange={(e) => setFirstname(e.target.value)}
        />
        {formSubmitted && errors.firstname && (
          <p className="text-red-500 text-sm">{errors.firstname}</p>
        )}

        <label className="block text-sm font-medium mt-7">Email *</label>
        <Input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        {formSubmitted && errors.email && (
          <p className="text-red-500 text-sm">{errors.email}</p>
        )}

        <label className="block text-sm font-medium mt-7">Mot de passe *</label>
        <Input
          type="password"
          placeholder="Mot de passe"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        {formSubmitted && errors.password && (
          <p className="text-red-500 text-sm">{errors.password}</p>
        )}

        <label className="block text-sm font-medium mt-7">
          Confirmer le mot de passe *
        </label>
        <Input
          type="password"
          placeholder="Confirmer le mot de passe"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />
        {formSubmitted && errors.confirmPassword && (
          <p className="text-red-500 text-sm">{errors.confirmPassword}</p>
        )}

        <Button
          className={`w-full mt-10 bg-primary hover:bg-opacity-80 transition duration-300 ${
            registerMutation.isPending ? "opacity-50 cursor-not-allowed" : ""
          }`}
          onClick={handleRegister}
          disabled={registerMutation.isPending}
        >
          {registerMutation.isPending
            ? "Création du compte..."
            : "Créer mon compte"}
        </Button>

        <p className="text-sm text-center mt-2">
          Vous avez déjà un compte Scryn ?{" "}
          <a href="/login" className="text-white font-bold hover:underline">
            Connectez-vous
          </a>
        </p>
      </Card>
    </div>
  );
};

export default RegisterPage;
