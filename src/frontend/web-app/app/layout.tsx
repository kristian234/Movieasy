import { Metadata } from "next";
import "./globals.css";
import 'react-multi-carousel/lib/styles.css';
import Provider from "./Providers/Provider";
import RefreshClientComponent from "./components/shared/refresh-component";

export const metadata: Metadata = {
    title: "Movieasy",
    description: "Movieasy",
};

export default async function RootLayout({
    children,
}: {
    children: React.ReactNode
}) {
    return (
        <html lang="en">
            {children}

        </html>
    )
}