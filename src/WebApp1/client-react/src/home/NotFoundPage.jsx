import {Link} from "react-router-dom";
const NotFoundPage = () =>{
    return (
        <div>
            <h1>Not Found!</h1>
            <Link to={"/"}>
                <button> Go to Home page </button>
            </Link>
        </div>
    )
}
export default NotFoundPage;