import {MdMenu} from "react-icons/md";
import "./Menu.scss"
import {Link} from "react-router-dom";

export default function Menu() {
    return (
        <>
            <div className="menu-container">
                <div className="menu">
                    <div className="menu-icon">
                        <MdMenu></MdMenu>
                    </div>
                    <div className="menu-body">
                        <Link  to="/">Home</Link>
                        <Link to="/about">About</Link>
                        <Link to="/register">Register</Link>
                    </div>
                </div>
            </div>
        </>
    )
}