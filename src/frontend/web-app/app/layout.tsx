import { Metadata } from "next";
import "./globals.css";
import Header from "./layout/header";
import { getServerSession } from "next-auth";
import Provider from "./Provider";

export const metadata: Metadata = {
  title: "Movieasy",
  description: "Movieasy",
};

interface Props {
  session: any;
  children: React.ReactNode;
}

export default async function RootLayout({ children }: Props) {
  return (
    <html lang="en">
      <body>
        <Provider>
          <Header /> 
          <body>{children}</body>
        </Provider>
      </body>
    </html>
  );
}
