'use client'

import LoginButton from "./headerComponents/login";
import Logo from "./headerComponents/logo";
import Search from "./headerComponents/search";

export default function Header() {
    return (
        <nav className="bg-header p-4 flex items-center justify-between shadow-2xl">
            <Logo />
            <Search />
            <LoginButton/>
        </nav>
    )
}