import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import Input from "../../../components/Input";
import { useLogin } from "../../../hooks/useAuth";

const LoginPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errors, setErrors] = useState<{ [key: string]: string }>({});
  const [formSubmitted, setFormSubmitted] = useState(false);
  const navigate = useNavigate();

  const loginMutation = useLogin();

  useEffect(() => {
    if (formSubmitted) {
      const newErrors: { [key: string]: string } = {};
      if (!email.trim()) newErrors.email = "L'email est requis.";
      if (!password.trim()) newErrors.password = "Le mot de passe est requis.";
      setErrors(newErrors);
    }
  }, [email, password, formSubmitted]);

  const handleLogin = () => {
    setFormSubmitted(true);
    if (!email || !password) return;

    loginMutation.mutate(
      { email, password },
      {
        onSuccess: () => navigate("/"),
        onError: (error: any) => {
          setErrors({ general: error.message || "Erreur de connexion" });
        },
      }
    );
  };

  return (
    <div className="flex flex-col lg:flex-row items-center justify-center min-h-screen text-white px-6 gap-12 lg:gap-32">
      <Card className="w-full max-w-md p-6 sm:p-8 lg:p-10">
        <h2 className="text-2xl font-bold text-center">Connexion</h2>

        {errors.general && (
          <p className="text-red-500 text-sm text-center mt-2">
            {errors.general}
          </p>
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

        <Button
          className={`w-full mt-10 bg-primary hover:bg-opacity-80 transition duration-300 ${
            loginMutation.isPending ? "opacity-50 cursor-not-allowed" : ""
          }`}
          onClick={handleLogin}
          disabled={loginMutation.isPending}
        >
          {loginMutation.isPending ? "Connexion..." : "Continuer"}
        </Button>

        <p className="text-sm text-center mt-2">
          Vous n'avez pas de compte Scryn ?{" "}
          <a href="/register" className="text-white font-bold hover:underline">
            Inscrivez-vous
          </a>
        </p>
      </Card>
    </div>
  );
};

export default LoginPage;
