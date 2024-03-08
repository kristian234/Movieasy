'use client'

import { signIn, useSession } from "next-auth/react";
import { useState } from "react";

export default function LoginForm() {
    const [email, setUsername] = useState("");
    const [password, setPassword] = useState("");


    const session = useSession();
    const handleSubmit = async (e : any) => {
        e.preventDefault();
        // "username-login" matches the id for the credential
        const result = await signIn("credential", { email, password, redirect: false});

        console.log(result);
        console.log(session);
      };
    

    return (
        <form onSubmit={handleSubmit} className="flex flex-col justify-center items-center">
            <div >
                <label htmlFor="email">Email</label>
                <input
                    id="email"
                    name="email"
                    type="text"
                    placeholder="Email"
                    onChange={(e) => setUsername(e.target.value)}
                    value={email}
                />
            </div>
            <div>
                <label htmlFor="password">Password</label>
                <input
                    id="password"
                    name="password"
                    type="password"
                    placeholder="Password"
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                />
            </div>
            <button type="submit">
                Login
            </button>
        </form>
    )
}