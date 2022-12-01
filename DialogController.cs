using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject dialog;
    public GameObject prophecy;
    public IEnumerator Intro1()
    {
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "The clamorous sounds of locomotives screeching to a halt; the restless metronome of shoes clacking upon concrete sidewalks; and the pounding of brass from unfinished construction all bore down on Taylor’s senses.",
            "Narrator",
            "Narrator_1_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "His mind remained addled, and his thoughts ran wild with emotions he lacked the maturity to internalize.",
            "Narrator",
            "Narrator_1_2");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He found himself alone, on the verge of tears, within the shadows of an empty alleyway.",
            "Narrator",
            "Narrator_1_3");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Mom... Dad... I... I...",
            "Taylor",
            "Taylor_1_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Something stalked him beyond the corners of his blurry eyes.",
            "Narrator",
            "Narrator_1_4");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Before he could become aware of their presence, they were already crouched beside him.",
            "Narrator",
            "Narrator_1_5");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Hmmm..",
            "Nyx",
            "Nyx_1_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Taylor stumbled back in surprise–fear writ plain across his face.",
            "Narrator",
            "Narrator_1_6");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "At ease, I mean you no harm.",
            "Nyx",
            "Nyx_1_2");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "She paused for a moment as she eyed his prosthetic arms. A mixture of disappointment and relief weighed down on her expression–but she spoke softly.",
            "Narrator",
            "Narrator_1_7");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Tell me, what is your name?",
            "Nyx",
            "Nyx_1_3");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "It’s–it’s Taylor…",
            "Taylor",
            "Taylor_1_2");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I’m Nyx. A pleasure.",
            "Nyx",
            "Nyx_1_4");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "She extended her arm for a handshake, but Taylor didn’t respond to her gesture.",
            "Narrator",
            "Narrator_1_8");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "*Sigh*",
            "Nyx",
            "Nyx_1_5");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Listen, Taylor, I’m here to help you.",
            "Nyx",
            "Nyx_1_6");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I know what happened to your village–to your family–and the ones responsible for it.",
            "Nyx",
            "Nyx_1_7");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "And I know you want revenge for what they did to them.",
            "Nyx",
            "Nyx_1_8");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "W-why do you want to help me? Who even are you?",
            "Taylor",
            "Taylor_1_3");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I’m a priestess of Nelumbo, and I have seen your fate.",
            "Nyx",
            "Nyx_1_9");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I–I don’t trust you, you’re a religious freak just like that priest…",
            "Taylor",
            "Taylor_1_4");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Contempt furrowed in her brow.",
            "Narrator",
            "Narrator_1_9");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "That shriveled excuse of a man is a traitorous wretch undeserving of the title, “priest.”",
            "Nyx",
            "Nyx_1_10");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Incredible as it may be, you’re the Chosen One: the person we’ve been waiting for all this time.",
            "Nyx",
            "Nyx_1_11");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "The people who ravaged your village? That priest who gave the order? Their deaths will scrub the deepest sins left on this earth–and the coming of peace is destined to be ushered in by your hands.",
            "Nyx",
            "Nyx_1_12");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I can awaken your powers–give you the revenge you seek.",
            "Nyx",
            "Nyx_1_13");
            // Narrator: Taylor didn’t know what to think–what to answer with–but the fervent urge for the priest’s demise boiled beneath his skin.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Taylor didn’t know what to think–what to answer with–but the fervent urge for the priest’s demise boiled beneath his skin.",
            "Narrator",
            "Narrator_1_10");
            // Taylor: Okay… 
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Okay…",
            "Taylor",
            "Taylor_1_5");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Good.",
            "Nyx",
            "Nyx_1_14");

        // go to tutorial scene
        SceneController.loadScene("Tutorial");

    }

    public void ShowProphecy()
    {
        prophecy.SetActive(true);
        dialog.GetComponent<Dialog>().HidePanel();
    }

    public void HideProphecy()
    {
        prophecy.SetActive(false);
        dialog.GetComponent<Dialog>().ShowPanel();
        
    }

    public IEnumerator Intro2(){
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "So what do we do next?",
            "Taylor",
            "Taylor_3_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Let me read you the prophecy first. The prophecy functions as a guide for the chosen one.",
            "Nyx",
            "Nyx_2_1");
        
        ShowProphecy();

        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "",
            "Nyx",
            "Nyx_prophecy");

        HideProphecy();

        // Nyx: That prophecy is about you Taylor. You are the one who came from soil and contains unknown power.

        // Nyx: According to the prophecy, you must first collect mithril. You’ll find lots of it in the mechanical body parts factory. But.. They won’t give it up willingly, so be prepared to fight for it.

        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "That prophecy is about you Taylor. You are the one who came from soil and contains unknown power.",
            "Nyx",
            "Nyx_2_2");

        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "According to the prophecy, you must first collect mithril. You’ll find lots of it in the mechanical body parts factory. But.. They won’t give it up willingly, so be prepared to fight for it.",
            "Nyx",
            "Nyx_2_3");
        SceneController.loadScene("StageSelection");
    }

    public IEnumerator Interlude(){
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "So what do we do next?",
            "Taylor",
            "Taylor_3_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "According to the prophecy, the last thing you must get is a ring that is made of the strangest substance. It doesn’t say what it is or what it’s for, only that it’s in the “old mausoleum”. ",
            "Nyx",
            "Nyx_3_1");
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I think I know where that is. An old ruin that was discovered to be an ancient mausoleum. But be careful, the prophecy says that they aren’t dead, whatever that means.",
            "Nyx",
            "Nyx_3_2");
        SceneController.loadScene("StageSelection");
    }

    public IEnumerator Interlude2(){
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I guess it's time for revenge.",
            "Taylor",
            "Taylor_35");
        SceneController.loadScene("StageSelection");
    }

    public IEnumerator PriestDialog(){
        // Priest: You still think you’re just a poor farm boy, I see? I doubt you even know what you’re truly attempting to achieve.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "You still think you’re just a poor farm boy, I see? I doubt you even know what you’re truly attempting to achieve.",
            "John",
            "Priest_1");
        // Priest: The founder of Nelumbo and its purpose are lost on you. Otherwise, you’d have stopped your foolish quest.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "The founder of Nelumbo and its purpose are lost on you. Otherwise, you’d have stopped your foolish quest.",
            "John",
            "Priest_2");
        // Priest: He was a gifted man, I’ll give him that, but all he accomplished was creating weapons that became the tools of terrorists.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He was a gifted man, I’ll give him that, but all he accomplished was creating weapons that became the tools of terrorists.",
            "John",
            "Priest_3");
        // Priest: He was obsessed with his family–obsessed with bringing them back from the grave. Nelumbo, that little cult of his, was part of his plan to resurrect them.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He was obsessed with his family–obsessed with bringing them back from the grave. Nelumbo, that little cult of his, was part of his plan to resurrect them.",
            "John",
            "Priest_4");

        // Priest: But after countless lives lost in his pursuit he realized that was impossible, and decided to create you, to destroy the world.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "But after countless lives lost in his pursuit he realized that was impossible, and decided to create you, to destroy the world.",
            "John",
            "Priest_5");
        // Priest: In the end, you’re nothing but a puppet wearing the skin of a boy. A mechanical parasite, who, if left to their own devices and their own delusions of self, would lead our planet to ruin.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "In the end, you’re nothing but a puppet wearing the skin of a boy. A mechanical parasite, who, if left to their own devices and their own delusions of self, would lead our planet to ruin.",
            "John",
            "Priest_6");
        // Priest: I tracked you into that poor rural community. I did what I had to… I tried to eliminate the problem from its root but I underestimated you and you escaped.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I tracked you into that poor rural community. I did what I had to… I tried to eliminate the problem from its root but I underestimated you and you escaped.",
            "John",
            "Priest_7");
        // Priest: But now it's time to finish what I started
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "But now it's time to finish what I started",
            "John",
            "Priest_8");
        SceneController.loadScene("StageSelection");
    }

    public IEnumerator FinalDialog(){
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "I need to know the truth–all of it. You have to tell me the truth, or else…",
            "Taylor",
            "Taylor_4_1");
        // Nyx: Lucas, Nelumbo’s founder, was a brilliant scientist and engineer.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Lucas, Nelumbo’s founder, was a brilliant scientist and engineer.",
            "Nyx",
            "Nyx_4_1");
        // Nyx: He found evidence of an ancient civilization, much more advanced than our own, that was destroyed long before our existence.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He found evidence of an ancient civilization, much more advanced than our own, that was destroyed long before our existence.",
            "Nyx",
            "Nyx_4_2");
        // Nyx: Before he could prove his findings to his colleagues, his son died of an unknown disease.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Before he could prove his findings to his colleagues, his son died of an unknown disease.",
            "Nyx",
            "Nyx_4_3");
        // Nyx: Shortly after his son’s death, his wife committed suicide out of grief.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Shortly after his son’s death, his wife committed suicide out of grief.",
            "Nyx",
            "Nyx_4_4");
        // Nyx: Lucas couldn’t cope with their departures. He became convinced that the ancient civilization he found would hold the technology required to bring his family back.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Lucas couldn’t cope with their departures. He became convinced that the ancient civilization he found would hold the technology required to bring his family back.",
            "Nyx",
            "Nyx_4_5");
        // Nyx: He explored many ruins of the world in search of said technology. His peers treated him as if he was no longer in his right mind–even deciding to strip him of his scientific position.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He explored many ruins of the world in search of said technology. His peers treated him as if he was no longer in his right mind–even deciding to strip him of his scientific position.",
            "Nyx",
            "Nyx_4_6");
        // Nyx: Soon after, he founded Nelumbo, and we would follow him in his searches.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Soon after, he founded Nelumbo, and we would follow him in his searches.",
            "Nyx",
            "Nyx_4_7");
        // Nyx: Many things were discovered in their queries, which in turn grew his cult.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Many things were discovered in their queries, which in turn grew his cult.",
            "Nyx",
            "Nyx_4_8");
        // Nyx: However, although much about the ancient civilization was learned about, none of it would aid Lucas in making his family live once more.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "However, although much about the ancient civilization was learned about, none of it would aid Lucas in making his family live once more.",
            "Nyx",
            "Nyx_4_9");
        // Nyx: At this point, he also found the true cause of death for his son and many others: lead poisoning.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "At this point, he also found the true cause of death for his son and many others: lead poisoning.",
            "Nyx",
            "Nyx_4_10");
        // Nyx: This is what matters, Taylor. I wasn’t lying when I told you that the world would become a better place without the priest or the government he serves …
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "This is what matters, Taylor. I wasn’t lying when I told you that the world would become a better place without the priest or the government he serves …",
            "Nyx",
            "Nyx_4_11");
        // Nyx: People are dying from their corruption–from the toxic smoke of factories to industrial waste that harms the soil. Why do you think that village of yours tried its best to find their own way of life outside the city?
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "People are dying from their corruption–from the toxic smoke of factories to industrial waste that harms the soil. Why do you think that village of yours tried its best to find their own way of life outside the city?",
            "Nyx",
            "Nyx_4_12");
        // Nyx: Lucas wanted to fix this–he wanted to save others the grief that he and many others had gone through. So he sought to make our planet a better place.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Lucas wanted to fix this–he wanted to save others the grief that he and many others had gone through. So he sought to make our planet a better place.",
            "Nyx",
            "Nyx_4_13");
        // Nyx: He put together a plan to get rid of the pollution, by revolutionizing the entire system. He came up with many ideas and prototypes, proof of concepts. His system was cheaper and way more effective, with a bigger output, and easily scalable.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He put together a plan to get rid of the pollution, by revolutionizing the entire system. He came up with many ideas and prototypes, proof of concepts. His system was cheaper and way more effective, with a bigger output, and easily scalable.",
            "Nyx",
            "Nyx_4_14");
        // Nyx: He presented this plan to the government, but they laughed in his face and stole his ideas to use on their military instead of preventing the deaths of so many people!
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "He presented this plan to the government, but they laughed in his face and stole his ideas to use on their military instead of preventing the deaths of so many people!",
            "Nyx",
            "Nyx_4_15");
        // Nyx: After this, Lucas began to formulate a way to overthrow the government.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "After this, Lucas began to formulate a way to overthrow the government.",
            "Nyx",
            "Nyx_4_16");
        // Nyx: The weapons created by the ancient civilization couldn’t be used by anyone.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "The weapons created by the ancient civilization couldn’t be used by anyone.",
            "Nyx",
            "Nyx_4_17");
        // Nyx: Only those with a certain genome could use them–someone like you.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "Only those with a certain genome could use them–someone like you.",
            "Nyx",
            "Nyx_4_18");
        // Nyx: This is why you’re the chosen one–why the mechanical parasite chose you.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "This is why you’re the chosen one–why the mechanical parasite chose you.",
            "Nyx",
            "Nyx_4_19");
        // Nyx: You’re the only one who can help us save our planet from losing any more innocent lives.
        yield return dialog.GetComponent<Dialog>().DialogCoroutine(
            "You’re the only one who can help us save our planet from losing any more innocent lives.",
            "Nyx",
            "Nyx_4_20");
        SceneController.loadScene("StageSelection");
    }



    void Start(){
        if (SceneController.dialog == "Intro1"){
            StartCoroutine(Intro1());
        }
        else if (SceneController.dialog == "Intro2"){
            Debug.Log("Intro2");
            StartCoroutine(Intro2());
        }
        else if (SceneController.dialog == "Interlude"){
            StartCoroutine(Interlude());
        }
        else if (SceneController.dialog == "Interlude2"){
            StartCoroutine(Interlude2());
        }
        // else if (SceneController.dialog == "PriestDialog"){
        //     StartCoroutine(PriestDialog());
        // }
        else if (SceneController.dialog == "FinalDialog"){
            StartCoroutine(FinalDialog());
        }
        // else break
        else {
            Debug.Log("No dialog");
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            if (SceneController.dialog == "Intro1"){
            SceneController.loadScene("Tutorial");
            }
            else if (SceneController.dialog == "Intro2"){
                SceneController.loadScene("StageSelection");
            }
            else if (SceneController.dialog == "Interlude"){
                SceneController.loadScene("StageSelection");
            }
            else if (SceneController.dialog == "Interlude2"){
                SceneController.loadScene("StageSelection");
            }
            else if (SceneController.dialog == "PriestDialog"){
                // SceneController.loadScene("StageSelection");
            }
            else if (SceneController.dialog == "FinalDialog"){
                SceneController.loadScene("StageSelection");
            }
        }
    }
}
