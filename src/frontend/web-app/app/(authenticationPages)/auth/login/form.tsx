'use client'
import { setCookie } from "cookies-next";
import { getSession, signIn } from "next-auth/react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useState } from "react";

export default function LoginForm() {
    const router = useRouter();
    const [email, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);

    const handleSubmit = async (e: any) => {
        e.preventDefault();

        setCookie('rememberMe', rememberMe);

        const result = await signIn("credential", { email, password, redirect: false });
        const session = await getSession();

        console.log(session);
        if (!result?.error) {
            router.push("/")
            router.refresh();
        }
    };

    return (
        <div className="flex justify-center p-6 mt-10">
            <div className="w-full bg-header rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0">
                <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                    <h1 className="text-xl font-bold leading-tight tracking-tight text-secondary md:text-2xl">
                        Sign in to your account
                    </h1>
                    <form onSubmit={handleSubmit} className="space-y-4 md:space-y-6" action="#">
                        <div>
                            <label htmlFor="email" className="block mb-2 text-sm font-bold text-third">Your email</label>
                            <input autoComplete='off' onChange={(e) => setUsername(e.target.value)} value={email} type="email" name="email" id="email" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " placeholder="name@company.com" />
                        </div>
                        <div>
                            <label htmlFor="password" className="block mb-2 text-sm font-bold text-third">Password</label>
                            <input onChange={(e) => setPassword(e.target.value)} value={password} type="password" name="password" id="password" placeholder="••••••••" className="bg-gray-50 border border-secondary text-gray-900 sm:text-sm rounded-lg focus:ring-secondary focus-ring-6 block w-full p-2.5 " />
                        </div>
                        <div className="flex items-center justify-between">
                            <div className="flex items-start">
                                <div className="flex items-center h-5">
                                    <input checked={rememberMe} onChange={(e) => setRememberMe(e.target.checked)} id="rememberMe" aria-describedby="rememberMe" type="checkbox" className="w-4 h-4 border form-checkbox text-secondary accent-gray-700 border-secondary rounded focus:outline-none focus:ring-0 focus:ring-third" />
                                </div>
                                <div className="ml-3 text-sm">
                                    <label htmlFor="remember" className=" text-third">Remember me</label>
                                </div>
                            </div>
                        </div>
                        <button type="submit" className="w-full text-body bg-secondary hover:bg-third focus:ring-1 focus:outline-none focus:ring-third font-bold rounded-lg text-sm px-5 py-2.5 text-center">Sign in</button>
                        <p className="text-sm font-medium text-third">
                            Don’t have an account yet? <Link href="/auth/register" className="font-bold text-secondary hover:underline">Sign up</Link>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    )
}