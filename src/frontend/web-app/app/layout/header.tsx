import { getServerSession } from "next-auth";
import Logo from "./headerComponents/logo";
import Search from "./headerComponents/search";
import LoginButton from "./headerComponents/login";
import LogoutButton from "./headerComponents/logout";

export default async function Header() {
    const session = await getServerSession();
    return (
        <nav className="bg-header p-4 flex items-center justify-between shadow-2xl">
            <Logo />
            <Search />
            {session?.user && !session.error? (
                <LogoutButton />

            ) : (
                < LoginButton />
            )}
        </nav>
    )
}