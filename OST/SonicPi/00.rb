
use_bpm 100
use_synth :prophet

define :pattern do |notes, a|
  
  play notes[0], amp: a
  sleep 0.5
  play notes[0], amp: a
  sleep 0.5
  
  
  i = 1
  4.times do
    play notes[0], amp: a
    sleep 0.25
    play notes[i], amp: a
    sleep 0.5
    
    i = (inc i)
  end
  
  
end


in_thread(name: :rythm) do
  sync :beat
  with_fx :reverb, mix: 0.05 do
    with_fx :echo, mix: 0.05 do
      loop do
        pattern [:G2,:G1,:G2,:G1,:G1], 0.5
      end
    end
  end
end

in_thread(name: :melody) do
  sync :beat
  loop do
    1.times do
      pattern [:G3,:B3,:C4,:Cs4,:D4], 0.5
    end
    1.times do
      pattern [:G3,:B3,:Cs4,:C4,:As3], 0.5
    end
    1.times do
      pattern [:G3,:B3,:C4,:Cs4,:D4], 0.5
    end
    1.times do
      pattern [:G3,:B3,:C4,:Cs4,:G4], 0.5
    end
  end
end


in_thread(name: :drums) do
  loop do
    cue :beat
    sample :drum_bass_hard
    sleep 1.0
  end
end


##| in_thread(name: :bass) do
##|   sync :beat
##|   use_synth :pluck

##|   with_fx :flanger do
##|     with_fx :slicer do

##|       r = 0.5
##|       att = 0.01
##|       n = :G4
##|       p = 0.0
##|       a = 2.0
##|       bn = 0

##|       define :p_note do
##|         play n, attack: att, release: r, pan: rrand(-1.0, 1.0), amp: a
##|       end

##|       loop do

##|         p = rrand(-1.0, 1.0)
##|         3.times do
##|           p_note
##|           sleep 0.25
##|         end
##|         sleep 0.25

##|         p_note
##|         sleep 0.5

##|         2.times do
##|           p_note
##|           sleep 0.25
##|         end

##|         sleep 0.5

##|         if bn == 3
##|           6.times do
##|             p_note
##|             sleep 0.25
##|           end
##|           bn = 0
##|         else
##|           p_note
##|           sleep 0.75
##|           p_note
##|           sleep 0.75
##|           bn = (inc bn)
##|         end
##|       end
##|     end
##|   end
##| end
