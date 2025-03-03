# MCTranslator
 마인크래프트 모드를 자동으로 번역해주는 프로그램입니다.
 GPT API를 이용하는 프로그램이며, 라이브러리 모드나 이미 한글 파일이 있는 경우 번역하지 않습니다.
 기본적으로 %appdata% .minecraft\mods 폴더의 모드들을 번역합니다만, 언제든 변경할 수 있습니다.

 사용 방법은 아래와 같습니다.

 1. 먼저 '폴더 선택'을 눌러 minecraft/mods 폴더를 선택한 뒤, GPT API 키를 입력해 줍니다.
 ![example1](https://github.com/MintSoda07/MCTranslator/blob/main/%EC%98%88%EC%8B%9C%201.png)

 2. 자동으로 번역할 수 없는 모드는 제외되고, 번역 대상 모드가 왼쪽 리스트에 정렬됩니다. 번역을 원치 않는 모드만 오른쪽으로 제외시킨 뒤 번역 시작을 눌러 번역을 시작합니다.
 ![example2](https://github.com/MintSoda07/MCTranslator/blob/main/%EC%98%88%EC%8B%9C%202.png)

 3. 번역이 시작되면 자동으로 GPT를 이용한 번역을 시도합니다. 성공적으로 번역될 경우, ko_kr.json 파일이 생성되어 .jar파일에 덮어쓰기됩니다.
 ![example3](https://github.com/MintSoda07/MCTranslator/blob/main/%EC%98%88%EC%8B%9C%203.png)

 4. 번역이 완료되면, 게임 내부에서 확인하여 줍니다. 어색한 번역이 존재한다면 고쳐줍시다.
 ![example4](https://github.com/MintSoda07/MCTranslator/blob/main/%EC%98%88%EC%8B%9C%204.png)

 모드 제작자의 허락 없이 '수정된 모드 파일'을 배포하는 것은 불법이므로, 번역된 모드는 개인적인 용도로만 사용해 주시기 바랍니다.
 향후 요청이 있다면 다른 언어로의 번역 기능을 확장할 예정입니다.
 감사합니다.
