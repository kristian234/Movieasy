import { getServerSession } from "next-auth";
import Logo from "../components/header/logo";
import Search from "../components/header/search";
import LoginButton from "../components/header/login";
import LogoutButton from "../components/header/logout";

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