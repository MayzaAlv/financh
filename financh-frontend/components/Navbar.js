import { Box, Flex } from "@chakra-ui/react";
import Image from "next/image";
import logo from "src/public/Capturar-removebg-preview.png";

export default function Navbar() {
  return (
    <Flex h="50px" px={6} py={2} bgColor="#19ED7A" margin="0 auto">
      <Box>
        <Image src={logo} width={226} heigth={30} />
      </Box>
    </Flex>
  );
}
