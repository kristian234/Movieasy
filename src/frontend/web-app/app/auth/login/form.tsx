'use client'
import { getCurrentUser, getTokenWorkaround } from "@/app/actions/auth-actions";
import { getSession, signIn, useSession } from "next-auth/react";
import { useState } from "react";

export default function LoginForm() {
    const [email, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = async (e : any) => {
        e.preventDefault();

        await signIn("credential", { email, password, redirect: false});
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