import {Sidebar} from "../components/admin/Sidebar";
import {Outlet} from "react-router-dom";
import Logo from "../assets/logo-glow.png";

const AdminLayout = () => {

    return (
        <div className="flex">
            <div className="fixed top-4 left-4">
                <img src={Logo} alt="Logo" className="w-[50%] h-auto"/>
            </div>
            <div className="ml-[400px] mt-[10vh] p-8 flex-grow">
                <Sidebar/>

                <Outlet/>
            </div>
        </div>
    );
};

export default AdminLayout;