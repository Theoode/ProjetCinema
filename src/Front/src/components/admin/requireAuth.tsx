import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

const isAuthenticated = () => {
    return localStorage.getItem("authToken") !== null;
};

const RequireAuth = ({ children }: { children: JSX.Element }) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuthenticated()) {
            navigate("/login");
        }
    }, [navigate]);

    return isAuthenticated() ? children : null;
};

export default RequireAuth;