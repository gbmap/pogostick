
use_bpm 100
use_synth :prophet

define :pattern do |notes|
  play notes[0]
  sleep 1.0
  play notes[1]
  sleep 1.0
  play notes[2]
  sleep 1.0
  play notes[3]
  sleep 1.0
  play notes[4]
  sleep 1.0
  play notes[5]
  sleep 1.0
  ##| 4.times do
  ##|   play notes[0]
  ##|   sleep 0.5
  ##|   play notes[0]
  ##|   sleep 1.0
  ##| end
  
  ##| play notes[0]
  ##| sleep 0.5
  ##| play notes[0]
  ##| sleep 2.0
  
end


in_thread(name: :rythm) do
  with_fx :reverb do
    with_fx :echo do
      loop do
        pattern [:G2,:G1,:G2,:G1,:G1]
      end
    end
  end
end

##| in_thread(name: :melody) do
##|   loop do
##|     1.times do
##|       pattern [:G3,:B3,:C4,:Cs4,:D4]
##|     end
##|     1.times do
##|       pattern [:G3,:B3,:Cs4,:C4,:As3]
##|     end
##|   end
##| end

##| in_thread(name: :drums) do
##|   loop do
##|     sample :drum_bass_hard
##|     sleep 1.0
##|   end
##| end


##| in_thread(name: :bass) do
##|   use_synth :dtri

##|   #with_fx :flanger do

##|   r = 0.5
##|   att = 0.01
##|   n = :G3
##|   p = 0.0
##|   a = 3.0

##|   define :p_note do
##|     play n, attack: att, release: r, pan: rrand(-1.0, 1.0), amp: a
##|   end

##|   loop do
##|     p = rrand(-1.0, 1.0)
##|     3.times do
##|       p_note
##|       sleep 0.25
##|     end
##|     sleep 0.25

##|     p_note
##|     sleep 0.5

##|     2.times do
##|       p_note
##|       sleep 0.25
##|     end

##|     sleep 0.5

##|     p_note
##|     sleep 0.75
##|     p_note
##|     sleep 0.75
##|   end
##|   #end
##| end

