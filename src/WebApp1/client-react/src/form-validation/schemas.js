import * as yup from "yup";
export const registerSchema = yup.object(
    {
        email: yup.string().email().required("Email is required"),
        password: yup.string().min(8).required("Password is required"),
        confirmPassword: yup.string().min(8)
            .required("Confirm passwords is required")
            .test("passwordsMatch", "Passwords must match",
                function (value) {
                    return value === this.resolve(yup.ref('password'));
                })
    }
)

export const loginSchema = yup.object(
    {
        email: yup.string().email().required("Email is required"),
        password: yup.string().min(8).required("Password is required")
        
    }
)


   