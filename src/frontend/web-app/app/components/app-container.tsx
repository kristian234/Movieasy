import Body from "../layout/body";
import Footer from "../layout/footer";
import Header from "../layout/header";

export default function AppContainer() {
    return (
        <div className="min-h-screen max-h-screen overflow-auto flex-grow">
            <Header />

            <Body />

            <Footer />
        </div>
    )
}