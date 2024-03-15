import { Metadata } from "next";
import "../globals.css";
import Header from "../layout/header";
import Provider from "../Provider";
import RefreshClientComponent from "../components/shared/refresh-component";

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
      <body>
        <div className="min-h-screen flex-auto max-h-screen overflow-auto">
          <Provider>
            <RefreshClientComponent />
            <Header />
            <main>
              {children}
            </main>
          </Provider>
        </div>
      </body>
    </html>
  )
}
