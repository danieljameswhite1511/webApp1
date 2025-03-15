import {useLocation} from "react-router-dom";
import Menu from "./Menu.jsx";
import "./Header.scss"
export default function Header() {
    const location = useLocation();
    return (
        <div className="header">
            <div className="left">
                wa1{location.pathname}    
            </div>
            <div className="right">
                <Menu></Menu>
            </div>
        </div>
    )
}