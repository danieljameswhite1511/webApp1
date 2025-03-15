export default function Register() {
    return (
        <>
            <div className="card">
                <div className="card-body">
                    <form>
                        <div>
                            <input id="email" name="email" type="email" className="form-control" placeholder="Email" />
                        </div>
                        <div>
                            <input id="password" name="password" type="password" className="form-control" placeholder="Password" />
                        </div>
                    </form>
                </div>
            </div>
        </>
    )
}