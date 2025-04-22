import {useLocation} from "react-router-dom";
import Menu from "./Menu.jsx";
import "./Header.scss"
import {useContext} from "react";
import {AuthContext} from "../auth/providers/AuthProvider.jsx";
export default function Header() {
    const auth = useContext(AuthContext);
    console.log(auth.isAuthenticated);
    const location = useLocation();
    return (
        <div className="header">
            <div className="left">
                <span className="wa1">wa1</span>
                <span className="wa1-path">
                    {location.pathname}
                </span>
            </div>
            <div className="right">
                {auth.isAuthenticated ? 'Logout' : 'Login'}
            </div>
        </div>
    )
}