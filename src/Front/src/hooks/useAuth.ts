import { useMutation } from "@tanstack/react-query";

const API_URL = "http://35.181.160.232:5000/login";

const loginApi = async (credentials: { email: string; password: string }) => {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(credentials),
  });

  if (!response.ok) {
    throw new Error("Identifiants incorrects");
  }

  const data = await response.json();

  // Stocker correctement le token
  localStorage.setItem("token", data.accessToken);
  localStorage.setItem(
    "user",
    JSON.stringify({
      email: credentials.email,
      token: data.accessToken,
    })
  );

  return data;
};

export const useLogin = () => {
  return useMutation({
    mutationFn: loginApi,
  });
};
