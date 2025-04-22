import Card from "../Card/Card.jsx";
import Button from "../form-controls/Button.jsx";
import authService from "./services/auth.service.js";
import {useNavigate} from "react-router-dom";
import {useState} from "react";
import {useForm} from "react-hook-form";
import {yupResolver} from "@hookform/resolvers/yup";
import * as schemas from "../form-validation/schemas.js";
import UserService from "../users/user.service.js";

export default function Login(){
    const navigate = useNavigate();
    const [serverErrors, setServerErrors] = useState([]);
    const {
        register,
        handleSubmit,
        formState: { errors , isSubmitting},
        reset,
        getValues,
    } = useForm({
        resolver: yupResolver(schemas.loginSchema),
    });
    async function onSubmit(data) {
        setServerErrors([]);
        await authService.login(data.email, data.password)
            .then(async (response) => {
                
               // reset();
              //  navigate("/");
                await UserService.getUsers().then((users) => {
                    console.log(users);
                }).catch((error) => {
                    console.log(error);
                })
            }).catch((errors) => {
                setServerErrors(errors);
            });
    }

    return (
        <>
            <Card>
                <div className="m-5">
                    <div className="p-5" style={{ width: '400px' }}>
                        <form onSubmit={handleSubmit(onSubmit)}>
                            <div className="flex justify-between">
                                <label htmlFor="email">Email</label>
                                <input {...register("email")}
                                        type="email"
                                       className=" border  border-green-950"
                                       placeholder="Enter Email">
                                </input>
                            </div>
                            <div className="flex justify-between">
                                <label htmlFor="password">Password</label>
                                <input {...register("password")}
                                        type="password"
                                       className=" border  border-green-950"
                                       placeholder="Enter Password"/>
                            </div>
                            <div className="flex justify-end">
                                <Button label="Login"
                                        disabled={isSubmitting}
                                        onClick={handleSubmit(onSubmit)}>
                                </Button>
                            </div>
                        </form>
                        <div className="server-side-errors">
                            {serverErrors.length > 0 &&
                                serverErrors.map((error, index) => (
                                    <div key={index}>
                                        {error}
                                    </div>
                                ))}
                        </div>
                    </div>
                </div>
            </Card>
        </>
    )
}