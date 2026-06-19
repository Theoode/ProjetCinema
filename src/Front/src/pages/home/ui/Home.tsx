import { Footer } from "../../../components/Footer";
import { HeroSection } from "../../../components/HeroSection";
import { Navbar } from "../../../components/Navbar";
import { NewsSection } from "../../../components/NewsSection";
import NowPlayingSection from "../../../components/NowPlayingSection";
import Spacing from "../../../components/Spacing";
import UpcomingMoviesSection from "../../../components/UpcomingMoviesSection";

export const Home: React.FC = () => {
  return (
    <div className="text-white min-h-screen flex flex-col bg-background">
      <Navbar />
      <HeroSection />
      <div className="max-w-screen-2xl mx-auto w-full px-4">
        <Spacing />
        <NowPlayingSection />
        <Spacing />
        <NewsSection />
        <Spacing />
        <UpcomingMoviesSection />
      </div>
      <Footer />
    </div>
  );
};
