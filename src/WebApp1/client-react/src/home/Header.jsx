import {useLocation} from "react-router-dom";
import Menu from "./Menu.jsx";
import "./Header.scss"
export default function Header() {
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
                <Menu></Menu>
            </div>
        </div>
    )
}